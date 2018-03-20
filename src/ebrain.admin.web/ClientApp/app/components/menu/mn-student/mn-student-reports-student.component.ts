import { Component, OnInit, OnDestroy, TemplateRef, ViewChild } from '@angular/core';
import { fadeInOut } from '../../../services/animations';

@Component({
    selector: 'mn_student_reports_student',
    templateUrl: './mn-student-reports-student.component.html',
    styleUrls: ['./mn-student-reports-student.component.css'],
    animations: [fadeInOut]
})

export class mn_student_reports_studentComponent implements OnInit, OnDestroy {
    ngOnDestroy() { }
    ngOnInit() { }
}
