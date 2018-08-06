using Studion.Models;

namespace Studion.Dtos
{
    public class ExamBoardDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ExamBoardDto(ExamBoard examBoardInDb)
        {
            this.Id = examBoardInDb.ExamBoardID;
            this.Name = examBoardInDb.ExamBoardName;
        }
    }
}