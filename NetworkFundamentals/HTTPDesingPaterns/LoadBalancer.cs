using System.Collections.Concurrent;

namespace NetworkFundamentals.HTTPDesingPaterns
{
    public class LoadBalancer
    {
        private readonly string[] _serverList;
        private int _roundRobinIndex;
        private readonly object _lockObj = new object();
        private readonly ConcurrentDictionary<string, int> _connectionCounts;
        private static readonly HttpClient _httpClient = new HttpClient(); // Statik HttpClient, yeniden kullanılabilir ve performans iyileştirir

        public LoadBalancer(string[] serverList)
        {
            _serverList = serverList;
            _roundRobinIndex = 0;
            _connectionCounts = new ConcurrentDictionary<string, int>(_serverList.ToDictionary(s => s, s => 0));
        }

        // Round-robin algoritmasını kullanarak bir sonraki sunucuyu seçen metot
        public string GetServerRoundRobin()
        {
            lock (_lockObj) // Bu bölüme aynı anda sadece bir iş parçacığının erişebilmesini sağla
            {
                var selectedServer = _serverList[_roundRobinIndex]; // Mevcut sunucuyu seç
                _roundRobinIndex = (_roundRobinIndex + 1) % _serverList.Length; // İndeksi bir sonraki sunucuya güncelle
                return selectedServer;
            }
        }

        // En az bağlantıya sahip sunucuyu seçen metot
        public string GetServerLeastConnections()
        {
            return _connectionCounts.OrderBy(kvp => kvp.Value).First().Key; // En az aktif bağlantıya sahip sunucuyu seç
        }

        // İsteği hedef sunucuya yönlendiren ve bağlantı sayısını yöneten metot
        public async Task ForwardRequestAsync(string targetServer, HttpRequestMessage requestMessage)
        {
            bool success = false;
            _connectionCounts[targetServer]++; // Seçilen sunucu için bağlantı sayısını artır
            try
            {
                var response = await _httpClient.SendAsync(requestMessage); // İsteği hedef sunucuya yönlendir
                Console.WriteLine($"Response from {targetServer}: {(int)response.StatusCode}");
                success = response.IsSuccessStatusCode; // Başarılı yanıt olup olmadığını kontrol et
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request to {targetServer} failed: {ex.Message}");
            }
            finally
            {
                if (!success)
                {
                    _connectionCounts[targetServer]--; // İstek başarısızsa bağlantı sayısını azalt
                }
            }
        }

        // Tüm sunucuların mevcut yük durumunu gösteren metot
        public void DisplayLoadStatus()
        {
            Console.WriteLine("Mevcut Yük Durumu:");
            foreach (var server in _serverList)
            {
                int totalConnections = _connectionCounts[server]; // Sunucu için aktif bağlantı sayısını al
                double loadPercentage = (double)totalConnections / Math.Max(1, _connectionCounts.Values.Sum()) * 100; // Yük yüzdesini hesapla
                Console.WriteLine($"Sunucu: {server}, Bağlantılar: {totalConnections}, Yük: {loadPercentage:F2}%");
            }
        }
    }
}

