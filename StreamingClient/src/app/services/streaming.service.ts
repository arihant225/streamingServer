import { Injectable } from '@angular/core';
import { ConfigService } from './config.service';

@Injectable({
  providedIn: 'root'
})
export class StreamingService {

  constructor(private configService:ConfigService) { }

  getMediaUrl(media:String){
    return this.configService.apis.streamingServer+"/file/media/"+media;
  }
}
