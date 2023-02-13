import { Injectable } from '@angular/core'
import { NgxSpinnerService } from 'ngx-spinner'

@Injectable({
  providedIn: 'root',
})
export class BusyService {
  busyReqCount = 0

  constructor(private spinnerService: NgxSpinnerService) {}

  busy() {
    this.busyReqCount++
    this.spinnerService.show(undefined, {
      type: 'cube-transition',
      bdColor: 'rgba(172,172,172,0.8)',
      color: '#df8e8e',
    })
  }

  idle() {
    this.busyReqCount--
    if (this.busyReqCount <= 0) {
      this.busyReqCount = 0
      this.spinnerService.hide()
    }
  }
}
