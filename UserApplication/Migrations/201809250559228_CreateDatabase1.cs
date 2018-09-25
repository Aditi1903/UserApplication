namespace UserApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        CityName = c.String(),
                        StateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CityId)
                .ForeignKey("dbo.States", t => t.StateId, cascadeDelete: false) //Changes done
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        CountryId = c.Int(nullable: false),
                        StateId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        Zipcode = c.Int(nullable: false),
                        Country_ContryId = c.Int(),
                    })
                .PrimaryKey(t => t.AddressId)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .ForeignKey("dbo.Countries", t => t.Country_ContryId)
                .ForeignKey("dbo.States", t => t.StateId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.StateId)
                .Index(t => t.CityId)
                .Index(t => t.Country_ContryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        ContryId = c.Int(nullable: false, identity: true),
                        CountryName = c.String(),
                    })
                .PrimaryKey(t => t.ContryId);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        StateId = c.Int(nullable: false, identity: true),
                        StateName = c.String(),
                        CountryId = c.Int(nullable: false),
                        Country_ContryId = c.Int(),
                    })
                .PrimaryKey(t => t.StateId)
                .ForeignKey("dbo.Countries", t => t.Country_ContryId)
                .Index(t => t.Country_ContryId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Gender = c.String(),
                        Hobbies = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        DOB = c.DateTime(nullable: false),
                        RoleId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        CourseName = c.String(),
                    })
                .PrimaryKey(t => t.CourseId);
            
            CreateTable(
                "dbo.SubjectInCourses",
                c => new
                    {
                        SubjectInCourseId = c.Int(nullable: false, identity: true),
                        SubjectId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SubjectInCourseId)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.SubjectId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        SubjectId = c.Int(nullable: false, identity: true),
                        SubjectName = c.String(),
                    })
                .PrimaryKey(t => t.SubjectId);
            
            CreateTable(
                "dbo.TeacherInSubjects",
                c => new
                    {
                        TeacherInSubjectId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TeacherInSubjectId)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.UserInRoles",
                c => new
                    {
                        UserInRoleId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserInRoleId)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)  //Changes done
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserInRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserInRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Users", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.TeacherInSubjects", "UserId", "dbo.Users");
            DropForeignKey("dbo.TeacherInSubjects", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.SubjectInCourses", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.SubjectInCourses", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Addresses", "UserId", "dbo.Users");
            DropForeignKey("dbo.States", "Country_ContryId", "dbo.Countries");
            DropForeignKey("dbo.Cities", "StateId", "dbo.States");
            DropForeignKey("dbo.Addresses", "StateId", "dbo.States");
            DropForeignKey("dbo.Addresses", "Country_ContryId", "dbo.Countries");
            DropForeignKey("dbo.Addresses", "CityId", "dbo.Cities");
            DropIndex("dbo.UserInRoles", new[] { "RoleId" });
            DropIndex("dbo.UserInRoles", new[] { "UserId" });
            DropIndex("dbo.TeacherInSubjects", new[] { "SubjectId" });
            DropIndex("dbo.TeacherInSubjects", new[] { "UserId" });
            DropIndex("dbo.SubjectInCourses", new[] { "CourseId" });
            DropIndex("dbo.SubjectInCourses", new[] { "SubjectId" });
            DropIndex("dbo.Users", new[] { "CourseId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.States", new[] { "Country_ContryId" });
            DropIndex("dbo.Addresses", new[] { "Country_ContryId" });
            DropIndex("dbo.Addresses", new[] { "CityId" });
            DropIndex("dbo.Addresses", new[] { "StateId" });
            DropIndex("dbo.Addresses", new[] { "UserId" });
            DropIndex("dbo.Cities", new[] { "StateId" });
            DropTable("dbo.UserInRoles");
            DropTable("dbo.Roles");
            DropTable("dbo.TeacherInSubjects");
            DropTable("dbo.Subjects");
            DropTable("dbo.SubjectInCourses");
            DropTable("dbo.Courses");
            DropTable("dbo.Users");
            DropTable("dbo.States");
            DropTable("dbo.Countries");
            DropTable("dbo.Addresses");
            DropTable("dbo.Cities");
        }
    }
}
