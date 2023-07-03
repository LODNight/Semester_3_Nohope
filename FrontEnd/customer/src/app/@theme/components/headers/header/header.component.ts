import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { UtilsService } from 'src/app/@core/services/utils.service';
import { ModalService } from 'src/app/@core/services/modal.service';
import { WishlistService } from 'src/app/@core/services/wishlist.service';

@Component({
  selector: 'molla-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})

export class HeaderComponent implements OnInit {

  @Input() containerClass = "container";

  wishCount = 0;

  constructor(
    public activeRoute: ActivatedRoute,
    public utilsService: UtilsService,
    public modalService: ModalService,
    public wishlistService: WishlistService
  ) {
  }

  ngOnInit(): void {
  }

  showLoginModal(event: Event): void {
    event.preventDefault();
    this.modalService.showLoginModal();
  }
}
