import $ from 'jquery'
import { CheckWebGPU } from "./helper";
import shader from './shaders/default.wgsl';
import '../dist/style.css'

$('#web-gpu-support').html(CheckWebGPU());

const CreateTriangle = async () => 
{
   const checkgpu = CheckWebGPU();

   if(checkgpu.includes("does not"))
   {
      console.log(checkgpu);
      throw(checkgpu);
   }

   const canvas = document.getElementById('canvas-webgpu') as HTMLCanvasElement;
   const adapter = await navigator.gpu?.requestAdapter() as GPUAdapter;
   const device = await adapter?.requestDevice() as GPUDevice;
   const context = canvas.getContext('webgpu') as GPUCanvasContext;
   const format = 'rgba8unorm';

   context.configure
   ({
      device: device,
      format: format
   })

   const pipeline = device.createRenderPipeline
   ({
      vertex:
      {
         module: device.createShaderModule
         ({
            code: shader
         }),
         entryPoint: "vs_main"
      },
      fragment:
      {
         module: device.createShaderModule({ code: shader}),
         entryPoint: "fs_main",
         targets: [{ format: format }]
      },
      primitive: { topology: "triangle-list" },
      layout: "auto"
   });

   const commandEncoder = device.createCommandEncoder();
   const textureView = context.getCurrentTexture().createView();
   const renderPass = commandEncoder.beginRenderPass
   ({
      colorAttachments:
      [{
         view: textureView,
         clearValue: { r: 0.2, g: 0.247, b: 0.314, a: 1.0},
         loadOp: "clear",
         storeOp: "store"
      }]
   });

   renderPass.setPipeline(pipeline);
   renderPass.draw(3, 1, 0, 0);
   renderPass.end();

   device.queue.submit([commandEncoder.finish()]);

}


CreateTriangle();

window.addEventListener('resize', function()
{
   CreateTriangle();
})