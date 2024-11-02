using AdminWebPage.Interfaces;

namespace AdminWebPage.Services
{
    public class BufferedFileUploadLocalService : IBufferedFileUploadService
    {
        /* async (Asynchronous) - await: Lập trình bất đồng bộ là khả năng thực thi các tác vụ độc lập nhau,
           (có thể chạy song song với nhau) giúp cho việc cải thiện hiệu suất hoạt động của ứng dụng
           ---> Đây là Phương pháp Lập trình đa luồng*/

        public async Task<bool> UploadFile(IFormFile file)
        {
            string path = "";
            try
            {
                if (file != null)
                {
                    path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "wwwroot", "User/img"));
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("File Copy Failed", ex);
            }
        }
    }
}
