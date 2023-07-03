import { Component, OnInit, Input } from '@angular/core';

import { newSlider } from '../data';

@Component({
  selector: 'molla-new-collection',
  templateUrl: './new-collection.component.html',
  styleUrls: ['./new-collection.component.scss']
})
export class NewCollectionComponent implements OnInit {

  @Input() products = [];
	@Input() loaded = false;

	categories = [['all'], ['accessories'], ['cameras', 'camcorders'], ['computers', 'tablets'], ['entertainment']];
  titles = ['All', 'Accessories', 'Cameras & Camcorders', 'Computers & Tablets', 'Entertainment']
  sliderOption = newSlider;
  
  constructor() { }

  ngOnInit(): void {
  }

}
