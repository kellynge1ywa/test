namespace Models;

public class ResponseDto
{
     public bool Success {get;set;}=true;
    public string Error {get;set;}="";
    public object Result {get;set;}=default!;

}
