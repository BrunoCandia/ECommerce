import { Component, Inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButton } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatDivider } from '@angular/material/divider';
import { MatListOption, MatSelectionList } from '@angular/material/list';
import { Type } from '../../../shared/models/type';
import { Brand } from '../../../shared/models/brand';

@Component({
  selector: 'app-filters-dialog',
  imports: [MatDivider, MatSelectionList, MatListOption, MatButton, FormsModule],
  templateUrl: './filters-dialog.component.html',
  styleUrl: './filters-dialog.component.scss'
})
export class FiltersDialogComponent implements OnInit {
  
  selectedTypes: Type[] = [];
  selectedBrands: Brand[] = [];
  // selectedBrands: string[] = [];
  // selectedTypes: string[] = [];

  // constructor(@Inject(MAT_DIALOG_DATA) public data: {brands: string[], types: string[], selectedBrands: string[], selectedTypes: string[]},
  //             private dialogRef: MatDialogRef<FiltersDialogComponent>) {}

  constructor(@Inject(MAT_DIALOG_DATA) public data: {brands: Brand[], types: Type[], selectedBrands: Brand[], selectedTypes: Type[]},
              private dialogRef: MatDialogRef<FiltersDialogComponent>) {}
  
  ngOnInit(): void {
    this.selectedBrands = this.data.selectedBrands;
    this.selectedTypes = this.data.selectedTypes;
    console.log('Dialog data:', this.data);
  }

  applyFilters() {
    this.dialogRef.close({
      selectedBrands: this.selectedBrands,
      selectedTypes: this.selectedTypes
    })
  }
}
