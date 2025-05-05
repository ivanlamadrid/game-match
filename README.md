HTML product list

<mat-card>
  <table mat-table [dataSource]="data">
    <ng-container matColumnDef="id">
      <th mat-header-cell *matHeaderCellDef>ID</th>
      <td mat-cell        *matCellDef="let p">{{p.id}}</td>
    </ng-container>

    <ng-container matColumnDef="title">
      <th mat-header-cell *matHeaderCellDef>Title</th>
      <td mat-cell        *matCellDef="let p">{{p.title}}</td>
    </ng-container>

    <ng-container matColumnDef="price">
      <th mat-header-cell *matHeaderCellDef>Price</th>
      <td mat-cell        *matCellDef="let p">\${{p.price}}</td>
    </ng-container>

    <tr mat-header-row *matHeaderRowDef="displayed"></tr>
    <tr mat-row        *matRowDef="let row; columns: displayed;"></tr>
  </table>
</mat-card>

TYPESCRUIPT

import {Component, OnInit} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MatTableModule} from '@angular/material/table';
import {MatCardModule} from '@angular/material/card';
import {FakestoreApiService} from '../../services/fakestore-api.service';
import {ProductEntity} from '../../model/product.entity';

@Component({
  selector: 'app-product-list',
  imports: [CommonModule, MatTableModule, MatCardModule],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.css'
})
export class ProductListComponent implements OnInit{
  data: ProductEntity[] = [];
  displayed = ['id','title','price'];

  constructor(private api: FakestoreApiService) {}

  ngOnInit() {
    this.api.getProducts().subscribe(p => this.data = p);
  }

}

MODEL TYPESCRITP:

import {RatingEntity} from './rating.entity';

export class ProductEntity {
  constructor(
    public id:          number = 0,
    public title:       string = '',
    public price:       number = 0,
    public description: string = '',
    public category:    string = '',
    public image:       string = '',
    public rating: RatingEntity = new RatingEntity(),
  ){}
}


FAKESTOREAPISERVICE TYPESCRIPT

import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {map, Observable} from 'rxjs';
import {ProductEntity} from '../model/product.entity';
import {ProductResponse} from './product.response';
import {ProductAssembler} from './product.assembler';
import {environment} from '../../../environments/environment.development';

@Injectable({ providedIn: 'root' })
export class FakestoreApiService {
  private http = inject(HttpClient);
  private baseUrl = environment.fakestoreProviderApiBaseUrl;
  private path = environment.fakestoreProviderProductsEndpointPath;

  getProducts(): Observable<ProductEntity[]> {
    return this.http       // <- Observable<ProductResponse[]>
      .get<ProductResponse[]>(`${this.baseUrl}${this.path}`)
      .pipe(
        map(resp => ProductAssembler.toEntityArray(resp))
      );
  }
}


PRODUCT ASSEMBLER

import {ProductResponse} from './product.response';
import {ProductEntity} from '../model/product.entity';
import {RatingEntity} from '../model/rating.entity';

export class ProductAssembler {
  static toEntity(resp: ProductResponse): ProductEntity {
    return new ProductEntity(
      resp.id,
      resp.title,
      resp.price,
      resp.description,
      resp.category,
      resp.image,
      new RatingEntity(resp.rating.rate, resp.rating.count)
    );
  }

  static toEntityArray(respArray: ProductResponse[]): ProductEntity[] {
    return respArray.map(this.toEntity);
  }
}


PRODUCT RESPONSE

export interface ProductResponse {
  id: number;
  title: string;
  price: number;
  description: string;
  category: string;
  image: string;
  rating: {
    rate: number;
    count: number;
  }
}


FOOTER

<mat-toolbar color="primary" class="mat-elevation-z2" style="justify-content:center;">
  © 2025 Fake Store API · DAOS Team
</mat-toolbar>


import { Component } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';

@Component({
  selector: 'app-footer-content',
  imports: [MatToolbarModule],
  templateUrl: './footer-content.component.html',
  styleUrl: './footer-content.component.css'
})
export class FooterContentComponent {

}


HEADER

<mat-toolbar color="primary">
  <button mat-icon-button class="example-icon" aria-label="Example icon-button with menu icon">
    <mat-icon>menu</mat-icon>
  </button>
  <span>New Fake Store</span>
  <span class="example-spacer"></span>
  <button mat-icon-button class="example-icon favorite-icon" aria-label="Example icon-button with heart icon">
    <mat-icon>favorite</mat-icon>
  </button>
  <button mat-icon-button class="example-icon" aria-label="Example icon-button with share icon">
    <mat-icon>share</mat-icon>
  </button>
  <span>
    futuro switcher
  </span>
</mat-toolbar>



import { Component } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';

@Component({
  selector: 'app-header-content',
  imports: [MatToolbarModule, MatButtonModule, MatIconModule],
  templateUrl: './header-content.component.html',
  styleUrl: './header-content.component.css'
})
export class HeaderContentComponent {

}

ROUTE

import { Routes } from '@angular/router';
import {ProductListComponent} from './products/components/product-list/product-list.component';

export const routes: Routes = [
  { path: '', component: ProductListComponent }
];

![image](https://github.com/user-attachments/assets/0d5450c5-a3c4-4a3c-9e8b-2b3438c5d76a)
