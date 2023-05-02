export const CheckWebGPU = () =>
{
   let result = "Your browser supports WebGPU!";
   if(!navigator.gpu) result = "Your browser does not support WebGPU!";
   return result;
}