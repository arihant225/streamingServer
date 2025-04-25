import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {

  public readonly apis={
    streamingServer:""
  }
  constructor() { 
    let windowRef=window as any;
    this.apis.streamingServer=windowRef["streamingEndpoint"];
  }
}
