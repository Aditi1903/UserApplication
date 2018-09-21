namespace UserApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateInitialDatabase : DbMigration
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
                        Address_AddressId = c.Int(),
                    })
                .PrimaryKey(t => t.CityId)
                .ForeignKey("dbo.Addresses", t => t.Address_AddressId)
                .Index(t => t.Address_AddressId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        ContryId = c.Int(nullable: false, identity: true),
                        CountryName = c.String(),
                        Address_AddressId = c.Int(),
                    })
                .PrimaryKey(t => t.ContryId)
                .ForeignKey("dbo.Addresses", t => t.Address_AddressId)
                .Index(t => t.Address_AddressId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        CourseName = c.String(),
                    })
                .PrimaryKey(t => t.CourseId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        StateId = c.Int(nullable: false, identity: true),
                        StateName = c.String(),
                        CountryId = c.Int(nullable: false),
                        Address_AddressId = c.Int(),
                    })
                .PrimaryKey(t => t.StateId)
                .ForeignKey("dbo.Addresses", t => t.Address_AddressId)
                .Index(t => t.Address_AddressId);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        SubjectId = c.Int(nullable: false, identity: true),
                        SubjectName = c.String(),
                    })
                .PrimaryKey(t => t.SubjectId);
            
            CreateTable(
                "dbo.SubjectInCourses",
                c => new
                    {
                        SubjectInCourseId = c.Int(nullable: false, identity: true),
                        SubjectId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SubjectInCourseId);
            
            CreateTable(
                "dbo.TeacherInSubjects",
                c => new
                    {
                        TeacherInSubjectId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TeacherInSubjectId);
            
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        CountryId = c.String(nullable: false),
                        StateId = c.String(nullable: false),
                        CityId = c.String(nullable: false),
                        Zipcode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AddressId);
            
            CreateTable(
                "dbo.UserInRoles",
                c => new
                    {
                        UserInRoleId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserInRoleId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Gender = c.String(nullable: false),
                        Hobbies = c.String(nullable: false),
                        Password = c.String(),
                        Email = c.String(nullable: false),
                        DOB = c.DateTime(nullable: false),
                        RoleId = c.Int(nullable: false),
                        AddressId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.AddressId)
                .Index(t => t.CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Users", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Users", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.States", "Address_AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Countries", "Address_AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Cities", "Address_AddressId", "dbo.Addresses");
            DropIndex("dbo.Users", new[] { "CourseId" });
            DropIndex("dbo.Users", new[] { "AddressId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.States", new[] { "Address_AddressId" });
            DropIndex("dbo.Countries", new[] { "Address_AddressId" });
            DropIndex("dbo.Cities", new[] { "Address_AddressId" });
            DropTable("dbo.Users");
            DropTable("dbo.UserInRoles");
            DropTable("dbo.Addresses");
            DropTable("dbo.TeacherInSubjects");
            DropTable("dbo.SubjectInCourses");
            DropTable("dbo.Subjects");
            DropTable("dbo.States");
            DropTable("dbo.Roles");
            DropTable("dbo.Courses");
            DropTable("dbo.Countries");
            DropTable("dbo.Cities");
        }
    }
}
