using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;



namespace HealthCalculator.Web.EntityModel

{

    public class TokenEntity

    {

        public int tokenId { get; set; }

        public Int64 userId { get; set; }

        public string authToken { get; set; }

        public System.DateTime issuedOn { get; set; }

        public System.DateTime expiresOn { get; set; }

    }

}