# 🚀 Network Fundamentals Projesi

Bu proje, **ağ protokollerini öğrenmek ve uygulamak** için tasarlanmış interaktif bir **C# konsol uygulamasıdır**. Uygulama, OSI modeli ve çeşitli ağ protokollerini içerir.

---

## 🎯 Amaç
📌 OSI modelinin katmanlarını anlamak<br>
📌 HTTP, FTP, WebSocket, TCP, UDP, IMAP, ARP, DHCP gibi protokolleri deneyimlemek<br>
📌 Konsol uygulamaları ile protokolleri simüle etmek ve geliştirmek<br>

---

## 📜 İçindekiler

🔹 **Ağ Protokolleri**
- [OSI Modeli](#osi-modeli)
- [HTTP & WebSocket](#http--websocket)
- [FTP](#ftp)
- [E-Posta Protokolleri (IMAP, SMTP, POP3)](#e-posta-protokolleri)
- [Ağ Katmanı Protokolleri (ARP, ICMP)](#ağ-katmanı-protokolleri)
- [Taşıma Katmanı Protokolleri (TCP, UDP)](#taşıma-katmanı-protokolleri)
- [Adresleme ve Ad Çözümleme (DHCP, DNS)](#adresleme-ve-ad-çözümleme)
- [Güvenlik Protokolleri (IPsec)](#güvenlik-protokolleri)

🔹 **Kurulum & Kullanım**
🔹 **Örnek Çıktılar**
🔹 **Katkıda Bulunma**
🔹 **Lisans**

---

## 🎮 Proje Menüsü

```bash
===== Network Protokolleri Uygulamasına Hoş Geldiniz =====
1. HTTP Protokolleri
2. FTP Protokolleri
3. E-Posta Protokolleri
4. Ağ Katmanı Protokolleri
5. Taşıma Katmanı Protokolleri
6. Adresleme ve Ad Çözümleme
7. Güvenlik Protokolleri
0. Çıkış
```

---

## 🔥 OSI Modeli

💡 **Bir HTTP isteği ile OSI modelini anlıyoruz!**

```csharp
OsiModel osiModel = new OsiModel();
await osiModel.MakeRequestAsync();
```

📌 **7 Katmanlı OSI Modeli:**
1. **Uygulama Katmanı**: Kullanıcı veri isteği başlatır.
2. **Sunum Katmanı**: JSON gibi formatları işler.
3. **Oturum Katmanı**: Bağlantıyı yönetir.
4. **Taşıma Katmanı**: TCP, UDP gibi protokolleri yönetir.
5. **Ağ Katmanı**: IP adresi yönlendirmesi yapar.
6. **Veri Bağlantısı Katmanı**: MAC adresleriyle iletişim sağlar.
7. **Fiziksel Katman**: Donanım seviyesinde veri iletimi yapar.

---

## 🔍 HTTP & WebSocket

💡 **HTTP ile bir siteye bağlanıyoruz!**

```csharp
HttpProtocolHandler handler = new HttpProtocolHandler();
await handler.Execute();
```

📌 **WebSocket Sunucu:**
```csharp
WebSocketServer server = new WebSocketServer("http://localhost:5000/ws/");
await server.StartAsync();
```

---

## 💾 Kurulum

Projeyi çalıştırmak için aşağıdaki adımları takip edin:

```bash
git clone https://github.com/kullanici/NetworkFundamentals.git
cd NetworkFundamentals
dotnet run
```

> **Gereksinimler:** .NET 8 SDK

---

## 💡 Örnek Çıktılar

```bash
Siteye bağlanılıyor...
Durum Kodu: 200
Site içeriği: {"userId": 1, "id": 1, "title": "delectus aut autem"}
```

---

## 🤝 Katkıda Bulunma
Katkıda bulunmak için:
1. 🍴 Projeyi forklayın.
2. 🔨 Değişikliklerinizi yapın.
3. 🛠️ Pull request gönderin.

---

