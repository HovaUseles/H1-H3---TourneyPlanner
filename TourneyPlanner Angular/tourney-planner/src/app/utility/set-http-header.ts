import { HttpHeaders } from "@angular/common/http";

export class SetHttpHeader {
  setAuthHeader(): HttpHeaders {
    const token = sessionStorage.getItem("token");
    return new HttpHeaders().set("Authorization", "Bearer " + token);
  }
}
