import { Directive, effect, ElementRef, Input, signal } from '@angular/core';
import HLS from 'hls.js'
import { StreamingService } from '../services/streaming.service';

@Directive({
  selector: '[appVideoStreamer]'
})
export class VideoStreamerDirective {
hls:HLS;
  constructor(
    private elementRef:ElementRef<HTMLVideoElement>,
    private streamingService:StreamingService
  ) { 
     this.hls=new HLS({
      maxBufferLength: 3,
      maxMaxBufferLength: 60, // Set a cap for the buffer length
     
      autoStartLoad:false
    });

    effect(
      ()=>{
        if(this._media()&&this._quality())
        {
          if(HLS.isSupported())
            {
              this.elementRef.nativeElement.pause()
              let time=this.elementRef.nativeElement.currentTime;
              console.log(time)
              this.hls.loadSource(this.streamingService.getMediaUrl(`${this._media()}${this._quality()}`))
              this.elementRef.nativeElement.currentTime=time
              this.hls.attachMedia(this.elementRef.nativeElement); 
          
              this.elementRef.nativeElement.play()
            }
        }
      }
    )
  }
  @Input() _quality=signal("");
  @Input() _media=signal("");

  @Input() set mediaSrc(media:string){
    this._media.set(media)
   
  }
  @Input() set quality(quality:number)
  {
    this._quality.set(`_${quality}p`)
  }

}
