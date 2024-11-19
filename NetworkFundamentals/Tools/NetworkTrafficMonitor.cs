using SharpPcap;

namespace NetworkFundamentals.Tools
{
    public class NetworkTrafficMonitor
    {
        private readonly ICaptureDevice _device;

        public NetworkTrafficMonitor(int deviceIndex)
        {
            //Tüm ağ cihazlarını listeleyin.

            var devices = CaptureDeviceList.Instance;

            if (devices.Count < 1)
            {
                throw new ArgumentException("Hiçbir Ağ Cihazı Bulunamadı");
            }

            if (deviceIndex >= devices.Count || deviceIndex < 0)
            {
                throw new ArgumentOutOfRangeException("Geçersiz cihaz seçimi.");

            }
            _device = devices[deviceIndex];
        }

        public void StartMonitoring()
        {

        }
    }
}
