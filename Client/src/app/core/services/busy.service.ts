import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class BusyService {
  loading = signal<boolean>(false);
  busyRequesCount = 0;

  constructor() { }

  busy() {
    this.busyRequesCount++;
    this.loading.set(true);
  }

  idle() {
    this.busyRequesCount--;

    if (this.busyRequesCount <= 0) {
      this.busyRequesCount = 0;
      this.loading.set(false);
    }
  }
}
