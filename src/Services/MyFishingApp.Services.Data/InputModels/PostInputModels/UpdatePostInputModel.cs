namespace MyFishingApp.Services.Data.InputModels.PostInputModels
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class UpdatePostInputModel
    {
        [Required]
        public int PostId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public IFormFileCollection FormFiles { get; set; }
    }
}
