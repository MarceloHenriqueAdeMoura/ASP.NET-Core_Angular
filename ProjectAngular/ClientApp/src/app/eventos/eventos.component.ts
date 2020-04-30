import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { error } from 'protractor';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
/** eventos component*/
export class EventosComponent implements OnInit {

  eventos: any;

  /** eventos ctor */
  constructor(private http: HttpClient) {

  }

  ngOnInit() {
    this.getEventos();
  }

  getEventos() {
    this.eventos = this.http.get('https://localhost:44309/api/eventos').subscribe(reponse => {
      this.eventos = reponse;
    }, error => {
      console.log(error);
    });
  }
}
