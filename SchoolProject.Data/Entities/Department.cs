using SchoolProject.Data.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
    public partial class Department : GeneralLocalizableEntity
    {
        public Department()
        {
            Students = new HashSet<Student>();
            DepartmentSubjects = new HashSet<DepartmentSubject>();
            Instructors = new HashSet<Instructor>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DID { get; set; }
        [MaxLength(200)]
        public string? DNameAr { get; set; }
        [MaxLength(200)]
        public string? DNameEn { get; set; }
        public int? InsManagerId { get; set; }
        [InverseProperty(nameof(Student.Department))]
        public virtual ICollection<Student> Students { get; set; }
        [InverseProperty("Department")]
        public virtual ICollection<DepartmentSubject> DepartmentSubjects { get; set; }
        [InverseProperty("Department")]
        public virtual ICollection<Instructor> Instructors { get; set; }
        [ForeignKey(nameof(InsManagerId))]
        [InverseProperty("ManagedDepartment")]
        public virtual Instructor? DepartmentManager { get; set; }
    }
}