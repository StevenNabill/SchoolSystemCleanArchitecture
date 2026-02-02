using SchoolProject.Data.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
    public class Subject : GeneralLocalizableEntity
    {
        public Subject()
        {
            StudentsSubjects = new HashSet<StudentSubject>();
            DepartmentsSubjects = new HashSet<DepartmentSubject>();
            InstructorsSubjects = new HashSet<InstructorSubject>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubID { get; set; }
        [StringLength(100)]
        public string? SubjectNameAr { get; set; }
        [StringLength(100)]

        public string? SubjectNameEn { get; set; }

        public int? Period { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<StudentSubject> StudentsSubjects { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<DepartmentSubject> DepartmentsSubjects { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<InstructorSubject> InstructorsSubjects { get; set; }
    }
}
