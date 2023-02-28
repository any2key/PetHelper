import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class FileLoaderAndReaderService {

  constructor() { }

  async onlyLoadFile(accept: string): Promise<File> {
    return new Promise((resolve, reject) => {
      let fileInput = document.createElement('input');
      [fileInput.type, fileInput.accept, fileInput.multiple] = ['file', accept, false];
      const listener = (event: Event) => {
        fileInput.removeEventListener('change', listener);
        fileInput = null;
        if ((<HTMLInputElement>event.target).files[0] ) {
          resolve((<HTMLInputElement>event.target).files[0]);
        };
      };
      fileInput.addEventListener('change', listener);
      fileInput.click();
    })
  }

  async readFileAsBase64(accept: string): Promise<{ name: string, data: string }> {
    const file = await this.onlyLoadFile(accept);
    return new Promise((resolve, reject) => {
      const reader = new FileReader()
      reader.onloadend = e => resolve({ name: file.name, data: btoa(e.target.result as string) });
      reader.onerror = e => reject(e);
      reader.readAsBinaryString(file);
    })
  }
}
