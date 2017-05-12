namespace HomeWorkfirst.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using System.Linq;
    using HomeWorkfirst.Models;

    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人 : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var db = new 客戶資料Entities();
            if ( 0 == this.Id ) { //表示沒有這筆資料
                if ( db.客戶聯絡人.Where(x => x.客戶Id == this.客戶Id && x.Email == this.Email ).Any()) {
                    yield return new ValidationResult("Email 已經存在", new string[] { "Email" });
                }
            }
            else {
                if (db.客戶聯絡人.Where(x => x.客戶Id == this.客戶Id && x.Id != this.Id && x.Email == this.Email).Any()) { //檢查
                    yield return new ValidationResult("Email 已經存在", new string[] { "Email" });
                }
            }
            yield return ValidationResult.Success;
        }
    }

    public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }

        [Required]
        //[Remote("RemoteEmailValidate", "客戶聯絡人", HttpMethod = "POST", AdditionalFields = "客戶Id, Email", ErrorMessage = "Email已存在")]
        public int 客戶Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 姓名 { get; set; }
        
        
        [Required]
        [EmailAddress(ErrorMessage = "Email格式不正確")]
        [StringLength(250, ErrorMessage = "欄位長度不得大於 250 個字元")]
        //[Remote("RemoteEmailValidate", "客戶聯絡人", AdditionalFields = "客戶Id, Email", ErrorMessage = "Email已存在")]
        public string Email { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [RegularExpression( @"\d{4}-\d{6}", ErrorMessage = "手機格式不正確，必須為 09xx-xxxxxx")]
        public string 手機 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }

        public bool 是否已刪除 { get; set; }

        public virtual 客戶資料 客戶資料 { get; set; }
    }
}
