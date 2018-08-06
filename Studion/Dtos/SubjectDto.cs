using Studion.Models;

namespace Studion.Dtos
{
    public class SubjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public SubjectDto(Subject subjectInDb)
        {
            this.Id = subjectInDb.SubjectID;
            this.Name = subjectInDb.SubjectName;
        }
    }
}