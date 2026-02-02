using SchoolProject.Data.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SchoolProject.Data.Entities
{
    public class Instructor : GeneralLocalizableEntity
    {
        public Instructor()
        {
            SuperVisedInstructors = new HashSet<Instructor>();
            InstructorsSubjects = new HashSet<InstructorSubject>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InsId { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? Address { get; set; }
        public string? Position { get; set; }
        public int? SuperVisorId { get; set; }
        public decimal? Salary { get; set; }
        public int DID { get; set; }
        [ForeignKey(nameof(DID))]
        [InverseProperty("Instructors")]
        public Department? Department { get; set; }

        [InverseProperty("DepartmentManager")]
        public Department? ManagedDepartment { get; set; }

        [InverseProperty("Instructor")]
        public virtual ICollection<InstructorSubject> InstructorsSubjects { get; set; }



        [ForeignKey(nameof(SuperVisorId))]
        [InverseProperty(nameof(SuperVisedInstructors))]
        public Instructor? SuperVisor { get; set; }

        [InverseProperty(nameof(SuperVisor))]
        public virtual ICollection<Instructor> SuperVisedInstructors { get; set; }
    }
}
