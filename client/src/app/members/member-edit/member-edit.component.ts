import { Component, HostListener, inject, OnInit, ViewChild, viewChild } from '@angular/core';
import { Member } from '../../_models/member';
import { MemberService } from '../../_services/member.service';
import { AccountService } from '../../_services/account.service';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { FormsModule, NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-member-edit',
  standalone: true,
  imports: [TabsModule, FormsModule],
  templateUrl: './member-edit.component.html',
  styleUrl: './member-edit.component.css'
})
export class MemberEditComponent implements OnInit{
 member?: Member;
 private memeberService = inject(MemberService);
 private accountServece = inject(AccountService);
 private toastr = inject(ToastrService);
 @ViewChild('editForm') editForm?: NgForm;
 @HostListener('window:beforeunload',['$event']) notify($event: any){
  debugger;
    if(this.editForm?.dirty){
      $event.returnValue = true;
    }
 }
  ngOnInit(): void {  
    this.loadMember();
  }
  loadMember(){
    const user = this.accountServece.currentUser();
    if(!user) return;
    this.memeberService.getMember(user.username).subscribe({
      next: member => {
        this.member = member
      }
    })
  }

  updateMember(){
    this.memeberService.updateMember(this.editForm?.value)
    .subscribe({
        next: _ => {
          this.toastr.success("Profile updated successfuly");
          this.editForm?.reset(this.member);
        }

    });
    
  }
}
