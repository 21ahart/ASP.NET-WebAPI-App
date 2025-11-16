using System.ComponentModel.DataAnnotations;


namespace MyWebApi.Models
{
    //Force one of these options (stored as int 0-4 - Change if str preferred).
    public enum YearInProgram
    {
        Freshman,
        Sophomore,
        Junior,
        Senior,
        Graduate
    }

    public class Student
    {
        [Key]
        public int Id { get; set; }  //primary key

        [Required]
        [MaxLength(100)]
        public string StudentName { get; set; } = string.Empty;

        public DateTime Birthdate { get; set; } // birthdate

        [MaxLength(100)]
        public string CollegeProgram { get; set; } = string.Empty; //Student college program

        public YearInProgram YearInProgram { get; set; } = YearInProgram.Freshman; //set freshman as default


    }
}
