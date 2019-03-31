import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { SearchResults } from "./products/SearchResults";
import { Observable } from "rxjs";
import { environment } from "./../environments/environment";

@Injectable({
  providedIn: "root"
})
export class ProductServiceService {
  constructor(private http: HttpClient) {}

  search(keyword): Observable<SearchResults> {
    return this.http.get<SearchResults>(
      environment.apiUrl + "/api/Product/Search?keyword=" + keyword
    );
  }
}
