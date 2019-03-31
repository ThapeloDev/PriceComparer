import { Pipe, PipeTransform } from '@angular/core';
import { Product } from './Product';

@Pipe({
  name: 'sortingProducts'
})
export class SortingProductsPipe implements PipeTransform {

  transform(products: Product[], path: string[], order: number): Product[] {
    
    // Check if is not null
    if (!products || !path || !order) 
    return products;

    return products.sort((a: Product, b: Product) => {
      // We go for each property followed by path
      path.forEach(property => {
        a = a[property];
        b = b[property];
      })

      // Order * (-1): We change our order
      return a > b ? order : order * (- 1);
    })
  }
}