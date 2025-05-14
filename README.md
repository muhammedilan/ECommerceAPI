
# Proje Başlığı

Genel E-Ticaret proje yapısında olabilecek yapıları kodladım. Proje öncelikle Onion Architecture mimarisiyle inşa edildi ve Generic Repository Pattern kullanıldı. Ardından proje büyüdükçe MediatR kütüphanesi kullanarak CQRS pattern entegre edildi. Güvenlik işlemleri için Identity ve JSON Web Token yapıları kullanıldı ve veritabanı işlemleri için Entity Framework Core ve PostgreSQL kullanıldı.

Projede API dokümantasyonu için Scalar kullandım ve Git commit'lerini temiz ve düzenli tuttum.

&nbsp;
## Kullanılan Teknolojiler
- C#
- .NET 9.0
- Visual Studio
- ASP.NET Core Web API
- Scalar API Documentation
- Entity Framework Core 9.0
- PostgreSQL
- Onion Architecture
- CQRS & MediatR
- Serilog & Seq
- SignalR
- Fluent Validation
- JSON Web Token
- AspNetCore.Identity
- Global Exception Handler

&nbsp;
## Ekran Görüntüleri

![Scalar API Docs](https://github.com/user-attachments/assets/854ad835-9310-4106-b6f4-e32efe1edc25)

&nbsp;
## Ortam Değişkenleri

Bu projeyi çalıştırmak için aşağıdaki ortam değişkenlerini appsettings.Development.json dosyanıza eklemeniz gerekecek

- Not: Gerçek proje geliştirme ortamında gizli bilgilerin development ortamındayken secrets.json'da ve production ortamındayken environment variables olarak verilmesi daha doğrudur. Ancak örnek bir proje yaptığım için bilerek appsettings.Development.json'a gizli bilgileri yerleştirdim.

`ConnectionStrings__PostgreSQL`

`Token__Audience`

`Token__Issuer`

`Token__SecurityKey`

`Seq__ServerURL`

&nbsp;
## Bilgisayarınızda Çalıştırın

Projeyi klonlayın

```bash
git clone https://github.com/muhammedilan/ECommerceAPI.git
```

Proje dizinine gidin

```bash
cd ECommerceAPI
```

Gerekli paketleri yükleyin

```bash
dotnet restore
```

Veritabanını hazırlayın

```bash
dotnet ef database update --project .\Infrastructure\ECommerceAPI.Persistence\ --startup-project .\Presentation\ECommerce.Api\
```

Sunucuyu çalıştırın

```bash
dotnet run --project .\Presentation\ECommerce.Api\
```

&nbsp;
## Geri Bildirim

Herhangi bir geri bildiriminiz varsa, lütfen muhammed.ilan.se@gmail.com adresinden bize ulaşın.

&nbsp;
