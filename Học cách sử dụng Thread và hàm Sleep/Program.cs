using System;

namespace MyApp{
    // tạo lớp Program
    public class Program{
        // hàm in ra thông báo
        // tham số thứ nhất n (tương đương với số lần in ra)
        // tham số thứ hai text (tương đương với dữ liệu in ra)
        // tham số thứ ba color (tương đương với màu sắc in ra)
        public static void ThongBao(int n, string text, ConsoleColor color){
            // đặt màu chữ
            // thành màu đỏ
            Console.ForegroundColor = color;
            
            for (int i = 1; i <= n; i++)
            {
                // tôi thích viết kiểu nội suy $""
                Console.Write($"{i} ");

                // dùng hàm Sleep()
                // để dừng 1 giây
                Thread.Sleep(1000);
            }

            // cài lại màu chữ mặc định
            Console.ResetColor();

            // in ra thông báo
            Console.WriteLine($"\n{text}");
        }

        // CHƯƠNG TRÌNH CHÍNH
        public static void Main(string[] args){
            // gọi hàm thông báo
            ThongBao(5, $"Su dung Thread va Sleep()", ConsoleColor.Red);

            Console.WriteLine();

            // gọi hàm thông báo lần nữa
            ThongBao(5, $"Welcome to C#", ConsoleColor.Green);

            // viết code như thế này
            // thì chưa được gọi là bất đồng bộ

            // Kết quả thì nó trông như này

            // 1    chữ màu đỏ
            // 2    chữ màu đỏ
            // 3    chữ màu đỏ
            // 4    chữ màu đỏ
            // 5    chữ màu đỏ
            // Su dung Thread va Sleep()
            // 1    chữ màu xanh lá
            // 2    chữ màu xanh lá
            // 3    chữ màu xanh lá
            // 4    chữ màu xanh lá
            // 5    chữ màu xanh lá
            // Welcome to C#

            // ngoài ra, người ta còn gọi đây là
            // lập trình đồng bộ
            // lập trình synchronous
            // các câu lệnh được thực thi tuần tự
            // trên 1 luồng xử lý
        }
    }
}