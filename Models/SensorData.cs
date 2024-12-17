using System;
using System.Globalization;

namespace Fitness.Models
{
    public class SensorData
    {
        public double AccelX { get; set; }
        public double AccelY { get; set; }
        public double AccelZ { get; set; }
        public double GyroX { get; set; }
        public double GyroY { get; set; }
        public double GyroZ { get; set; }
        public double Temperature { get; set; }
        public int Steps { get; set; }
        public string State { get; set; } = string.Empty;
        public int HeartRate { get; set; }

        public static SensorData? Parse(string data)
        {
            try
            {
                var sensorData = new SensorData();
                var pairs = data.Split(',');
                
                foreach (var pair in pairs)
                {
                    var keyValue = pair.Split('=');
                    if (keyValue.Length != 2) continue;

                    var key = keyValue[0].Trim().ToLower();
                    var value = keyValue[1].Trim();

                    switch (key)
                    {
                        case "ax":
                            if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double ax))
                                sensorData.AccelX = ax;
                            break;
                        case "ay":
                            if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double ay))
                                sensorData.AccelY = ay;
                            break;
                        case "az":
                            if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double az))
                                sensorData.AccelZ = az;
                            break;
                        case "gx":
                            if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double gx))
                                sensorData.GyroX = gx;
                            break;
                        case "gy":
                            if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double gy))
                                sensorData.GyroY = gy;
                            break;
                        case "gz":
                            if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double gz))
                                sensorData.GyroZ = gz;
                            break;
                        case "temp":
                            if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double temp))
                                sensorData.Temperature = temp;
                            break;
                        case "steps":
                            if (int.TryParse(value, out int steps))
                                sensorData.Steps = steps;
                            break;
                        case "state":
                            sensorData.State = value;
                            break;
                        case "hr":
                            if (int.TryParse(value, out int hr))
                                sensorData.HeartRate = hr;
                            break;
                    }
                }

                // 验证必要的数据是否存在
                if (sensorData.Temperature == 0 || sensorData.HeartRate == 0)
                {
                    return null;
                }

                return sensorData;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override string ToString()
        {
            return $"Temperature={Temperature:F2}°C, HeartRate={HeartRate}bpm, " +
                   $"Accel(X={AccelX:F2},Y={AccelY:F2},Z={AccelZ:F2}), " +
                   $"Steps={Steps}, State={State}";
        }
    }
} 