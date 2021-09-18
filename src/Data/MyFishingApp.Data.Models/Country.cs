namespace MyFishingApp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MyFishingApp.Data.Common.Models;

    public class Country : BaseDeletableModel<string>
    {
        public Country()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }
    }
}
