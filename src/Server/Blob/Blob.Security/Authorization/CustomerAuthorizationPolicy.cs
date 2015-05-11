//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Claims;
//using System.IdentityModel.Policy;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Blob.Security.Authorization
//{
//    class CustomerAuthorizationPolicy : IAuthorizationPolicy {
//  Guid _id = Guid.NewGuid();
//  // custom issuer claim set
//  ApplicationIssuerClaimSet _issuer = new ApplicationIssuerClaimSet();

//  public bool Evaluate(EvaluationContext evaluationContext, 
//    ref object state) {
//    Claim id = evaluationContext.ClaimSets.FindIdentityClaim();
//    string userId = Map(id);

//    evaluationContext.AddClaimSet(this, new CustomerClaimSet(userId));

//    return true;
//  }

//  public ClaimSet Issuer {
//    get { return _issuer; }
//  }

//  public string Id {
//    get { return 'CustomerAuthorizationPolicy: ' + _id.ToString(); }
//  }
//}
//}
