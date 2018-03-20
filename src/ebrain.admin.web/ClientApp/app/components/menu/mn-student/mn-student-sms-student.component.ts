import { Component, OnInit, OnDestroy, TemplateRef, ViewChild } from '@angular/core';
import { fadeInOut } from '../../../services/animations';

@Component({
    selector: 'mn_student_sms_student',
    templateUrl: './mn-student-sms-student.component.html',
    styleUrls: ['./mn-student-sms-student.component.css'],
    animations: [fadeInOut]
})

export class mn_student_sms_studentComponent implements OnInit, OnDestroy {
    ngOnDestroy() { }
    ngOnInit() { }
}
