using Infrastructure.Repository;
using Domain.Model;

namespace Tests;
public class GradeRepositoryTest
{
	[Fact]
    public void VoidTest()
	{
		TestHelper testHelper = new TestHelper();
		GradeRepository gradeRepository = testHelper.GradeRepository;

		Assert.Equal(1, 1);
	}

	[Fact]
	public void TestAdd()
	{
		TestHelper testHelper = new TestHelper();
		GradeRepository gradeRepository = testHelper.GradeRepository;

        Grade gradeForCreate = new Grade
        {
            Name = "Test2",
            GradeTeacher = new Teacher
            {
                FullName = "Test",
                Birthday = DateTime.UtcNow,
                Discipline = new Discipline
                {
                    Name = "Test",
                },
                PersonalLifeNumber = 123,
                PhoneNumber = "Test",
            },
            GradeType = new GradeType
            {
                Name = "Test",
            },
        };

        gradeRepository.Add(gradeForCreate);
        // Запрещаем отслеживание сущностей (разрываем связи с БД)
        testHelper.ChangeTrackerClear();

        List<Grade> grades = gradeRepository.getAll();
        Grade? createdGrade = grades.Find(grade => grade.Name == gradeForCreate.Name);

        Assert.Equal(2, grades.Count);
        Assert.NotNull(createdGrade);
	}

    [Fact]
    public void TestUpdate()
    {
        TestHelper testHelper = new TestHelper();
        GradeRepository gradeRepository = testHelper.GradeRepository;

        List<Grade> grades = gradeRepository.getAll();
        Grade grade = grades[0];
        string newName = "UpdatedTest";

        grade.Name = newName;

        gradeRepository.Update(grade);
        // Запрещаем отслеживание сущностей (разрываем связи с БД)
        testHelper.ChangeTrackerClear();

        grades = gradeRepository.getAll();

        Assert.Equal(newName, grades[0].Name);
        Assert.Single(grades);
    }

    [Fact]
    public void TestDelete()
    {
        TestHelper testHelper = new TestHelper();
        GradeRepository gradeRepository = testHelper.GradeRepository;

        List<Grade> grades = gradeRepository.getAll();
        Grade grade = grades[0];

        gradeRepository.Delete(grade);
        // Запрещаем отслеживание сущностей (разрываем связи с БД)
        testHelper.ChangeTrackerClear();

        grades = gradeRepository.getAll();

        Assert.Empty(grades);
    }
}


