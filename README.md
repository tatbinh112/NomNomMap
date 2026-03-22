<<<<<<< HEAD
# NomNomMap
Khám phá ẩm thực quanh bạn — Nền tảng bản đồ ẩm thực cộng đồng, giúp người dùng tìm kiếm, đánh giá và chia sẻ các quán ăn gần vị trí của mình.
=======
# 🍜 NomNom Map

> **Khám phá ẩm thực quanh bạn** — Nền tảng bản đồ ẩm thực cộng đồng, giúp người dùng tìm kiếm, đánh giá và chia sẻ các quán ăn gần vị trí của mình.

---

## ✨ Tính năng

### Người dùng không đăng ký
- 🗺️ Xem bản đồ quán ăn theo vị trí hiện tại
- 🔍 Tìm kiếm & lọc quán theo danh mục, khoảng cách, đánh giá
- 📸 Xem ảnh, bài viết review và thông tin chi tiết quán

### Người dùng đã đăng ký
- ❤️ Like / Unlike quán yêu thích
- ✍️ Viết bài đánh giá & chấm điểm
- 📷 Upload ảnh quán ăn
- 📍 Tạo địa điểm quán ăn mới trên bản đồ

---

## 🛠️ Tech Stack

| Layer | Công nghệ |
|---|---|
| **Backend** | ASP.NET Core Web API (.NET 8) |
| **Frontend** | React + Vite + TypeScript |
| **Database** | PostgreSQL + PostGIS |
| **Bản đồ** | Leaflet.js + OpenStreetMap |
| **Auth** | ASP.NET Core Identity + JWT |
| **ORM** | Entity Framework Core |

---

## 📁 Cấu trúc dự án

```
nomnommap/
├── backend/                        # ASP.NET Core Web API
│   ├── NomNomMap.API/              # Entry point, Controllers, Middleware
│   ├── NomNomMap.Application/      # Business logic, Services, DTOs
│   ├── NomNomMap.Domain/           # Entities, Interfaces
│   ├── NomNomMap.Infrastructure/   # EF Core, Repositories, DB Context
│   └── NomNomMap.Tests/            # Unit & Integration tests
│
├── frontend/                       # React + Vite + TypeScript
│   ├── src/
│   │   ├── components/             # UI components (Map, Cards, Forms...)
│   │   ├── pages/                  # Các trang chính
│   │   ├── hooks/                  # Custom React hooks
│   │   ├── services/               # API calls
│   │   ├── store/                  # State management
│   │   └── types/                  # TypeScript interfaces
│   └── public/
│
├── docs/                           # Tài liệu, ERD, wireframes
├── docker-compose.yml
└── README.md
```

---

## 🚀 Khởi động dự án

### Yêu cầu
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Node.js 18+](https://nodejs.org/)
- [PostgreSQL 15+](https://www.postgresql.org/) với extension PostGIS
- [Docker](https://www.docker.com/) *(tùy chọn)*

### Chạy bằng Docker (khuyến nghị)

```bash
git clone https://github.com/your-username/nomnommap.git
cd nomnommap
docker-compose up -d
```

### Chạy thủ công

**Backend:**
```bash
cd backend/NomNomMap.API
dotnet restore
dotnet ef database update
dotnet run
# API chạy tại: http://localhost:5000
```

**Frontend:**
```bash
cd frontend
npm install
npm run dev
# App chạy tại: http://localhost:5173
```

---

## 🗃️ Database Schema

```
Users           → Id, Username, Email, PasswordHash, AvatarUrl, CreatedAt
Restaurants     → Id, Name, Address, Lat, Lng, Category, CreatedByUserId, CreatedAt
Reviews         → Id, RestaurantId, UserId, Rating (1-5), Content, CreatedAt
Photos          → Id, RestaurantId, UserId, ImageUrl, CreatedAt
Likes           → Id, RestaurantId, UserId, CreatedAt
```

> Geo-query sử dụng PostGIS `ST_DWithin()` để tìm quán trong bán kính cho trước.

---

## 📡 API Endpoints

```
GET    /api/restaurants?lat=&lng=&radius=    # Lấy quán gần vị trí
GET    /api/restaurants/{id}                 # Chi tiết quán
POST   /api/restaurants                      # Tạo quán mới (cần auth)

GET    /api/restaurants/{id}/reviews         # Danh sách review
POST   /api/restaurants/{id}/reviews         # Viết review (cần auth)

POST   /api/restaurants/{id}/likes           # Like quán (cần auth)
DELETE /api/restaurants/{id}/likes           # Unlike quán (cần auth)

POST   /api/auth/register                    # Đăng ký
POST   /api/auth/login                       # Đăng nhập
```

---

## 🗺️ Lộ trình phát triển

- [x] Thiết kế database schema & API
- [ ] Setup project backend + frontend
- [ ] Tích hợp bản đồ Leaflet + OpenStreetMap
- [ ] Geo-query tìm quán theo vị trí
- [ ] Hệ thống Auth (JWT)
- [ ] Tính năng Like, Review, Upload ảnh
- [ ] Tạo địa điểm mới
- [ ] Deploy MVP

---

## 🤝 Đóng góp

Pull requests và issues luôn được chào đón! Vui lòng đọc [CONTRIBUTING.md](docs/CONTRIBUTING.md) trước khi bắt đầu.

---

## 📄 License

[MIT](LICENSE)
>>>>>>> 29ed072 (docs: replace README with project version)
