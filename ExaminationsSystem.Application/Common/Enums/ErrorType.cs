using System.ComponentModel;

namespace ExaminationsSystem.Application.Common.Enums
{
    public enum ErrorType
    {
        [Description("The {0} is already exists")]
        Item_Alreday_Exist = 1,

        [Description("The {0} is not exists")]
        Item_NotExist,

        [Description("Studen exam is already started")]
        StudentExam_Alreday_Started,

        [Description("Student exam has ended")]
        StudentExam_Ended,

        [Description("Exam is expired")]
        Exam_expired,

        [Description("Exam is not available now ")]
        Exam_NotAvailable_Now,

        [Description("Studen exam is not started")]
        StudentExam_Not_Started,

        [Description("Studen exam is already ended")]
        StudentExam_Alreday_Ended,

        [Description("Studen exam is not ended")]
        StudentExam_Not_Ended,

        [Description("Question is not exist in the Exam")]
        Question_NotExistInExam,

        [Description("Selected pption is not exist in the question")]
        SelectedOptions_NotExistInQuestion,

        [Description("Studen exam is already graded")]
        StudentExam_Alreday_Graded,

        [Description("Student Exam")]
        StudentExam,

        [Description("Student")]
        Student,
        [Description("Exam")]
        Exam
    }
}
