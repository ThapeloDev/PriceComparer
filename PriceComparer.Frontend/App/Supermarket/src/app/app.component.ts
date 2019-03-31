import { Component, OnInit } from "@angular/core";
import { ProductServiceService } from "./product-service.service";
import { SearchResults } from "./products/SearchResults";
import { Product } from "./products/Product";
import { Supermarket } from "./products/Supermarket";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"]
})
export class AppComponent implements OnInit {
  title = "Supermarktprijzen vergelijken";
  originalResults: SearchResults = new SearchResults();
  results: SearchResults = new SearchResults();
  keyword = "gehakt";
  filter: string;
  path: string[] = ["Product"];
  order: number = 1; // 1 asc, -1 desc;
  searching: boolean;

  constructor(private productService: ProductServiceService) {}

  ngOnInit() {
    this.searching = true;
    this.productService.search("gehakt").subscribe(result => {
      this.originalResults = JSON.parse(JSON.stringify(result));
      this.results = { ...result };
      this.searching = false;
    });
  }

  onSubmit() {
    this.searching = true;
    this.productService.search(this.keyword).subscribe(result => {
      console.log(result);
      this.originalResults = JSON.parse(JSON.stringify(result));
      this.results = { ...result };
      this.searching = false;
    });
    
  }

  sort(prop: string) {
    this.path = prop.split(".");
    this.order = this.order * -1; // change order
    return false; // do not reload
  }

  filterResults() {
    this.results = JSON.parse(JSON.stringify(this.originalResults));

    this.results.Supermarkets.forEach(supermarket => {
      supermarket.Products = supermarket.Products.filter(
        p => p.Name.toLowerCase().indexOf(this.filter) > -1
      );
    });
  }
}
