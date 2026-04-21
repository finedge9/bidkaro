// =============================================
// Models/AuctionListing.cs
// =============================================
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BidKaro.Models
{
    [Table("AuctionListings")]
    public class AuctionListing
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(100)]
        public string Make { get; set; }

        [MaxLength(100)]
        public string Model { get; set; }

        public int Year { get; set; }

        [MaxLength(50)]
        public string Fuel { get; set; }           // Petrol / Diesel / EV / CNG

        [MaxLength(50)]
        public string Transmission { get; set; }   // Manual / Automatic

        [MaxLength(50)]
        public string Color { get; set; }

        public int KMs { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(50)]
        public string State { get; set; }

        [MaxLength(50)]
        public string Category { get; set; }       // 2-Wheeler, 4-Wheeler, Commercial, etc.

        [MaxLength(100)]
        public string Source { get; set; }         // Bank Repo, NBFC, Consumer, etc.

        [Column(TypeName = "decimal(18,2)")]
        public decimal BasePrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CurrentBid { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal MinIncrement { get; set; } = 5000;

        public int Bids { get; set; }

        public string EndsIn { get; set; }         // For display (HH:MM:SS)

        public DateTime AuctionEndTime { get; set; }

        public DateTime AuctionStartTime { get; set; }

        [MaxLength(50)]
        public string Status { get; set; }         // upcoming / live / closed / sold

        [MaxLength(50)]
        public string LotNumber { get; set; }

        public bool IsFeatured { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }


    // =============================================
    // Models/BidHistory.cs
    // =============================================
    [Table("BidHistory")]
    public class BidHistory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AuctionId { get; set; }

        [Required]
        public string BidderId { get; set; }       // Masked: "***45"

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public DateTime BidTime { get; set; } = DateTime.UtcNow;

        public bool IsWinning { get; set; }

        [ForeignKey("AuctionId")]
        public virtual AuctionListing Auction { get; set; }
    }


    // =============================================
    // Models/User.cs
    // =============================================
    [Table("Users")]
    public class User
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required, MaxLength(100)]
        public string FullName { get; set; }

        [Required, MaxLength(20)]
        public string Mobile { get; set; }

        [MaxLength(200)]
        public string Email { get; set; }

        [MaxLength(500)]
        public string PasswordHash { get; set; }

        public bool IsMobileVerified { get; set; }
        public bool IsKYCVerified { get; set; }
        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [MaxLength(20)]
        public string Role { get; set; } = "Buyer";  // Buyer / Seller / Admin
    }


    // =============================================
    // ViewModels/LoginViewModel.cs
    // =============================================
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter your mobile or email")]
        [Display(Name = "Mobile / Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }


    // =============================================
    // ViewModels/RegisterViewModel.cs
    // =============================================
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Full name is required")]
        [Display(Name = "Full Name")]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Mobile number is required")]
        [Phone(ErrorMessage = "Enter a valid 10-digit mobile number")]
        [Display(Name = "Mobile Number")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Range(typeof(bool), "true", "true", ErrorMessage = "Please accept the terms & conditions")]
        [Display(Name = "I agree to Terms & Conditions")]
        public bool AcceptTerms { get; set; }
    }


    // =============================================
    // Models/BidKaroDbContext.cs  (Entity Framework)
    // =============================================
    // using System.Data.Entity;
    //
    // public class BidKaroDbContext : DbContext
    // {
    //     public BidKaroDbContext() : base("DefaultConnection") { }
    //
    //     public DbSet<AuctionListing> AuctionListings { get; set; }
    //     public DbSet<BidHistory>     BidHistory      { get; set; }
    //     public DbSet<User>           Users            { get; set; }
    //
    //     protected override void OnModelCreating(DbModelBuilder modelBuilder)
    //     {
    //         modelBuilder.Entity<AuctionListing>()
    //             .HasMany<BidHistory>(a => a.BidHistories)
    //             .WithRequired(b => b.Auction)
    //             .HasForeignKey(b => b.AuctionId);
    //         base.OnModelCreating(modelBuilder);
    //     }
    // }
}
