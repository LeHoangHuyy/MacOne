# MacOne - Website Thương Mại Điện Tử ASP.NET Core MVC

---

## Giới thiệu

**MacOne** là một website thương mại điện tử mini bán các mặt hàng của **Apple** được xây dựng bằng **ASP.NET Core MVC**.
Dự án Được tổ chức theo kiến trúc **Repository - Service - Controller**, có tách riêng khu vực quản trị (Admin) và giao diện người dùng (User).

---

## Tính năng nổi bật

### Người dùng (User)

-   Đăng ký / Đăng nhập / Đăng xuất kết hợp sử dụng với session, cookies.
-   Xem danh mục sản phẩm.
-   Xem chi tiết sản phẩm.
-   Lọc sản phẩm theo danh mục.
-   Phân trang sản phẩm

### Quản trị viên (Admin)

-   Đăng nhập / Đăng xuất kết hợp sử dụng với session, cookies.
-   Thêm / Sửa / Xóa sản phẩm.
-   Upload ảnh khi thêm / sửa sản phẩm
-   Thêm / Sửa / Xóa danh mục
-   Phân trang sản phẩm
-   Tích hợp API nội bộ

---

## API Endpoints

### User API

| HTTP | Endpoint            | Mô tả                            |
| ---- | ------------------- | -------------------------------- |
| GET  | /api/user/home      | Lấy danh sách sản phẩm           |
| GET  | /api/user/home/{id} | Lấy danh sách sản phẩm theo loại |
| GET  | /api/user/shop      | Lấy danh sách tất cả sản phẩm    |
| GET  | /api/user/shop/{id} | Lấy danh sách sản phẩm theo loại |

### Admin API

#### Product API

| HTTP   | Endpoint                 | Mô tả                          |
| ------ | ------------------------ | ------------------------------ |
| GET    | /api/admin/products      | Lấy danh sách sản phẩm (Admin) |
| GET    | /api/admin/products/{id} | Lấy sản phẩm theo id           |
| POST   | /api/admin/products      | Tạo mới sản phẩm               |
| PUT    | /api/admin/products/{id} | Cập nhật sản phẩm              |
| DELETE | /api/admin/products/{id} | Xóa sản phẩm                   |

#### Category API

| HTTP   | Endpoint                   | Mô tả                       |
| ------ | -------------------------- | --------------------------- |
| GET    | /api/admin/categories      | Lấy danh sách loại sản phẩm |
| GET    | /api/admin/categories/{id} | Lấy loại sản phẩm theo id   |
| POST   | /api/admin/categories      | Tạo mới loại sản phẩm       |
| PUT    | /api/admin/categories/{id} | Cập nhật loại sản phẩm      |
| DELETE | /api/admin/categories/{id} | Xóa loại sản phẩm           |

---

## Công nghệ sử dụng

### Backend

-   **ASP.NET Core MVC 8.0.**
-   **Entity Framework Core.**
-   **SQL Server.**

### Frontend

-   **HTML, CSS, JavaScript.**
-   **jQuery, AJAX.**

---

## Hướng dẫn chạy dự án trên Visual Studio 2022

-   **Bước 1: Clone git repository về máy local**
-   **Bước 2: Cấu hình Database:** Mở file macone.sql có sẵn trong dự án trên VS2022 và Excuted để chạy toàn bộ script
-   **Bước 3: Cấu hình ConnectionString:**
    1. Tạo mới file appsettings.json.
    2. Copy toàn bộ nội dung từ file appsettings.Example.json sang appsettings.json.
    3. Cập nhật "YOUR_SERVER" bằng server name trên local của bạn.
