import { Claim } from "../interfaces/claim";

export class GetUserId {
  getUserId() {
    let claims: Claim = JSON.parse(atob(sessionStorage.getItem("token")!.split('.')[1]));
    return claims.UserId;
  };
};
