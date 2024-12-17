using System;
using System.IO.Ports;
using System.Text;
using Fitness.Models;
using System.Threading.Tasks;

namespace Fitness.Services
{
    public class SerialPortService : IDisposable
    {
        private SerialPort? _serialPort;
        private StringBuilder _dataBuffer = new StringBuilder();
        private bool _isRunning;
        public event Action<SensorData>? OnDataReceived;
        public event Action<string>? OnError;
        public bool IsConnected => _serialPort?.IsOpen ?? false;

        public SerialPortService(string portName = "COM8", int baudRate = 9600)
        {
            try
            {
                _serialPort = new SerialPort
                {
                    PortName = portName,
                    BaudRate = baudRate,
                    DataBits = 8,
                    Parity = Parity.None,
                    StopBits = StopBits.One,
                    ReadTimeout = 1000,
                    WriteTimeout = 1000,
                    DtrEnable = true,
                    RtsEnable = true,
                    NewLine = "\n"
                };

                _serialPort.DataReceived += SerialPort_DataReceived;
                _serialPort.ErrorReceived += SerialPort_ErrorReceived;
            }
            catch (Exception ex)
            {
                OnError?.Invoke($"初始化串口时发生错误: {ex.Message}");
            }
        }

        private void SerialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            OnError?.Invoke($"串口错误: {e.EventType}");
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (_serialPort?.IsOpen == true)
                {
                    string data = _serialPort.ReadLine();
                    Console.WriteLine($"收到数据: {data}");
                    
                    if (!string.IsNullOrWhiteSpace(data))
                    {
                        var sensorData = SensorData.Parse(data.Trim());
                        if (sensorData != null)
                        {
                            Console.WriteLine($"解析数据: {sensorData}");
                            OnDataReceived?.Invoke(sensorData);
                        }
                        else
                        {
                            Console.WriteLine("数据解析失败");
                        }
                    }
                }
            }
            catch (TimeoutException)
            {
                OnError?.Invoke("读取数据超时");
            }
            catch (Exception ex)
            {
                OnError?.Invoke($"接收数据时发生错误: {ex.Message}");
            }
        }

        public static string[] GetAvailablePorts()
        {
            return SerialPort.GetPortNames();
        }

        public bool Start()
        {
            try
            {
                if (_serialPort == null)
                {
                    OnError?.Invoke("串口未初始化");
                    return false;
                }

                if (!_serialPort.IsOpen)
                {
                    _serialPort.Open();
                    _isRunning = true;
                    Console.WriteLine("串口已打开");
                }
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                OnError?.Invoke("串口访问被拒绝，可能被其他程序占用");
                return false;
            }
            catch (Exception ex)
            {
                OnError?.Invoke($"打开串口时发生错误: {ex.Message}");
                return false;
            }
        }

        public void Stop()
        {
            try
            {
                _isRunning = false;
                if (_serialPort?.IsOpen == true)
                {
                    _serialPort.Close();
                    Console.WriteLine("串口已关闭");
                }
            }
            catch (Exception ex)
            {
                OnError?.Invoke($"关闭串口时发生错误: {ex.Message}");
            }
        }

        public void Dispose()
        {
            Stop();
            _serialPort?.Dispose();
        }
    }
} 