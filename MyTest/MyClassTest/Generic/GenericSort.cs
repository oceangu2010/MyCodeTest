using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyTest.MyClassTest
{
    public class GenericSort
    {
    }

    /// <summary>
    /// 学生类
    /// </summary>
    public class Student
    {
        private string name;
        // 姓名
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int age;
        // 年龄
        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        private string grade;
        // 年级
        public string Grade
        {
            get { return grade; }
            set { grade = value; }
        }

        //构造函数
        public Student(string name, int age, string grade)
        {
            this.name = name;
            this.age = age;
            this.grade = grade;
        }

        public override string ToString()
        {
            return this.name + "," + this.age.ToString() + "," + this.grade;
        }
    }

    public class StudentComparer : IComparer<Student>
    {
        public enum CompareType
        {
            Name,
            Age,
            Grade
        }

        private CompareType type;

        // 构造函数，根据type的值，判断按哪个字段排序
        public StudentComparer(CompareType type)
        {
            this.type = type;
        }

        #region IComparer<Student> 成员

        public int Compare(Student x, Student y)
        {
            switch (this.type)
            {
                case CompareType.Name:
                    return x.Name.CompareTo(y.Name);

                case CompareType.Age:
                    return x.Age.CompareTo(y.Age);

                default://case CompareType.Grade:
                    return x.Grade.CompareTo(y.Grade);
            }
        }

        #endregion
    }




}//end namespace