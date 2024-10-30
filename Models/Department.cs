namespace Asp.Net_MvcWeb_Pj3.Aptech.Models;
using System.ComponentModel.DataAnnotations; // Key


// mã thực thể: Nhân Viên Quản Trị
// 2024.10.02 15h40
public
class Department
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }
    public bool Invalid()
    {
        bool invalid = false;
        if(this.Name.Length < 2)
        {
            invalid = true;
            Write("Lỗi->tên không được ít hơn 2 ký tự.");
        }
        if(this.Name.Length > 32)
        {
            invalid = true;
            Write("Lỗi->tên không được lớn hơn 32 ký tự");
        }

        return invalid;
    }

}

