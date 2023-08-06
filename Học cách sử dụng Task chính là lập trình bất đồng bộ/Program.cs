// lập trình bất đồng bộ
// là asynchronous

// .NET nó cho phép chúng ta
// tạo ra nhiều tác vụ

// những cái tác vụ đấy
// có thể chạy song song với nhau
// chạy đồng thời với nhau

// cụ thể:
// cho 2 hàm
// hàm 1 chạy trên luồng số 1
// hàm 2 chạy trên luồng số 2
// nó chạy trên nhiều luồng khác nhau

// lớp để biểu diễn tác vụ
// trong .NET là lớp Task
// hoặc Task<T> với tham số generic tên là T


// Lý thuyết tạo đối tượng kiểu Task
// cách 1:
// chúng ta truyền vào biểu thức lambda
// Task dt = new Task(() => { });

// cách 2:
// tham số bên trái là biểu thức lambda
// tham số bên phải là đối tượng obj
// obj dùng để truyền vào tham số bên trái
// Task dt = new Task((Object obj) => { }, obj);

using System;

namespace MyApp
{
    // tạo lớp Program
    public class Program
    {
        // tạo hàm thông báo
        // tham số thứ nhất: số lần lặp n
        // tham số thứ hai: một dòng dữ liệu text
        // tham số thứ ba: dùng để cài đặt màu sắc cho chữ
        public static void ThongBao(int n, string text, ConsoleColor dt_color)
        {
            for (int i = 1; i <= n; i++)
            {
                lock(Console.Out){
                    // bây giờ tôi muốn câu lệnh
                    // Console.ForegroundColor = dt_color;
                    // không bị sử dụng bởi nhiều luồng
                    // thì tôi sẽ dùng lock để khóa nó lại

                    // cài đặt màu sắc
                    Console.ForegroundColor = dt_color;

                    Console.WriteLine($"{i} ");
                    Thread.Sleep(1000);

                    // đặt lại màu sắc
                    Console.ResetColor();
                }
            }

            lock(Console.Out){
                Console.ForegroundColor = dt_color;

                // tôi đang viết code
                // theo kiểu nội suy $"{tên_biến}"
                Console.WriteLine($"{text}");

                // đặt lại màu sắc
                Console.ResetColor();
            }
        }

        public static void Main(string[] args)
        {
            Console.Clear();

            // tạo đối tượng
            // có kiểu tác vụ
            Task dt1 = new Task(() => {
                ThongBao(5, "Tac vu 1", ConsoleColor.Red);
            });

            Task dt2 = new Task((object obj) => {
                // cái obj
                // sẽ hứng cái chuỗi "Tac vu 2"

                // bây giờ, tôi tạo biến str
                // hứng dữ liệu của obj
                // nhưng tôi sẽ ép kiểu sang string
                string str = (string)obj;

                // gọi hàm thông báo
                ThongBao(5, str, ConsoleColor.Green);

            }, "Tac vu 2");


            // để bảo cái đối tượng tác vụ
            // cho nó chạy
            // thì phải gọi phương thức Start()
            dt1.Start();        // đây là luồng chạy số 2
            dt2.Start();        // đây là luồng chạy số 3


            // đây là luồng chính
            ThongBao(5, "Tac vu 0", ConsoleColor.Blue);


            // KẾT QUẢ
            // có thể là:
            // Tac vu 1
            // Tac vu 2
            // Tac vu 0

            // cũng có thể là:
            // Tac vu 0
            // Tac vu 2
            // Tac vu 1

            // cũng có thể là:
            // Tac vu 2
            // Tac vu 0
            // Tac vu 1

            // vì nó chạy trên nhiều luồng
            // nên cái nào xong trước thì in ra trước
            // có lúc thì cái này nhanh hơn
            // có lúc thì cái kia nhanh hơn
            // nên kết quả in ra không thể biết chắc chắn được
        }
    }
}