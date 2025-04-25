import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { VideoStreamerDirective } from './directives/video-streamer.directive';

@Component({
  selector: 'app-root',
  imports: [VideoStreamerDirective],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'StreamingClient';
}
