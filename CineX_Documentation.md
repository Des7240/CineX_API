# TÀI LIỆU HƯỚNG DẪN SỬ DỤNG VÀ TÍCH HỢP CINEX API

## 1. TỔNG QUAN CÔNG NGHỆ (TECH STACK)
- **Framework**: .NET 9.0 (ASP.NET Core Web API)
- **Database**: PostgreSQL (Được host trên Neon.tech)
- **ORM**: Entity Framework Core 9
- **Kiến trúc API**: RESTful API kết hợp OData v4 (Cho phép linh hoạt $filter, $select, $expand)
- **Lưu trữ file**: Cloudflare R2 (S3-Compatible) thông qua AWSSDK.S3
- **Tài liệu API (UI)**: Scalar (Phiên bản mới thay thế Swagger trên .NET 9)
- **Môi trường**: Quản lý bằng biến môi trường (File `.env` trên local, Environment Variables trên server Render).

---

## 2. DANH SÁCH ENDPOINT API (ENDPOINT LIST)
*Base URL trên môi trường Production:* `https://cinex-api.onrender.com`

### 2.1. Quản lý Dự án (Projects)
- `GET /odata/Projects`: Lấy danh sách dự án.
- `POST /odata/Projects`: Tạo mới một dự án.
- `GET /odata/Projects?$expand=Acts`: Lấy dự án kèm theo danh sách các hồi (Acts).

### 2.2. Quản lý Hồi (Acts)
- `GET /odata/Acts`: Lấy danh sách hồi.
- `POST /odata/Acts`: Thêm mới hồi vào kịch bản.

### 2.3. Quản lý Phân cảnh (Scenes)
- `GET /odata/Scenes`: Lấy tất cả phân cảnh.
- `POST /odata/Scenes`: Tạo phân cảnh mới.
- `GET /odata/Scenes?$expand=Location,SceneCharacters($expand=Character)`: Lấy dữ liệu đầy đủ của phân cảnh (Bao gồm chi tiết bối cảnh và danh sách nhân vật). Rất thích hợp để hiển thị trên màn hình Dashboard.

### 2.4. Quản lý Nhân vật (Characters)
- `GET /odata/Characters`: Lấy danh sách nhân vật.
- `POST /odata/Characters`: Tạo mới nhân vật.

### 2.5. Quản lý Bối cảnh (Locations)
- `GET /odata/Locations`: Lấy danh sách bối cảnh.
- `POST /odata/Locations`: Tạo mới bối cảnh.

### 2.6. Upload File (Hình ảnh Poster/Avatar)
- `POST /api/FileUpload/upload`:
  - **Body (form-data)**: 
    - `file`: (File ảnh từ máy)
    - `prefix`: (Chuỗi - ví dụ: "poster" hoặc "avatar")
  - **Trả về**: JSON chứa URL public của file ảnh trên Cloudflare R2 để bạn lưu vào database (Trường PosterUrl hoặc ImageUrl).

### 2.7. Cập nhật và Xóa dữ liệu (Update & Delete)
Hệ thống **đã hỗ trợ đầy đủ** việc cập nhật và xóa cho tất cả các bảng (Projects, Acts, Scenes, Characters, Locations) thông qua các method tiêu chuẩn của OData. Cấu trúc dùng chung cho mọi bảng như sau:
- **Cập nhật (PATCH)**: `PATCH /odata/TênBảng(ID)`
  - *Ví dụ*: `PATCH /odata/Projects(1)`
  - *Cách dùng*: Gửi JSON body chứa **chỉ những trường bạn muốn thay đổi**. Hệ thống sẽ tự động map và update. (VD: `{"Title": "Tên phim mới"}`).
- **Xoá (DELETE)**: `DELETE /odata/TênBảng(ID)`
  - *Ví dụ*: `DELETE /odata/Projects(1)`
  - *Lưu ý*: Hệ thống đã cài đặt cơ chế **Cascade Delete**. Nghĩa là nếu bạn xoá 1 Project, tất cả các Act và Scene thuộc Project đó sẽ tự động bị xoá sạch sẽ khỏi database!

---

## 3. CÁCH KIỂM TRA (HOW TO TEST)

### Cách 1: Sử dụng giao diện tài liệu trực quan (Scalar)
- Mở trình duyệt và truy cập: `https://cinex-api.onrender.com/scalar/v1`
- Giao diện này cung cấp danh sách toàn bộ Endpoints. Bạn có thể bấm vào từng endpoint và chọn "Test Request" để điền body và gửi lệnh trực tiếp trên trình duyệt.

### Cách 2: Sử dụng Postman (Khuyên dùng)
Tôi đã cung cấp sẵn file `CineX_Postman_Collection.json`.
1. Mở ứng dụng Postman.
2. Bấm nút **Import** góc trên bên trái, kéo file JSON trên thả vào.
3. Một thư mục "CineX API (Production)" sẽ xuất hiện chứa sẵn toàn bộ các mẫu Request.
4. Bạn chỉ cần mở Request, chỉnh sửa tham số trong thẻ **Body** (nếu cần) và bấm **Send**. (Đối với API Upload File, hãy qua thẻ Body -> form-data -> chọn file từ máy tính).

---

## 4. HƯỚNG DẪN CHO FRONTEND
Hệ thống hiện tại đang được cấu hình dạng **Open API** (Không yêu cầu truyền Token ở Header).
- Khi lấy danh sách bảng, dùng HTTP GET.
- Để gửi dữ liệu JSON (Tạo mới), dùng HTTP POST kèm Header `Content-Type: application/json`.
- Khi cần lấy thông tin chi tiết nối bảng (Ví dụ lấy Scene kèm Location), hãy tận dụng sức mạnh của OData bằng cách thêm parameter `?$expand=TênBảng` vào cuối URL. Điều này giúp bạn giảm thiểu số lần phải gọi API xuống chỉ còn 1 lần duy nhất!
