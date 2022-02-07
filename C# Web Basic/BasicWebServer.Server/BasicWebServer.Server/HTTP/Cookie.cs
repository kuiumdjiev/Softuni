using BasicWebServer.Server.Common;

public class Cookie
    {

    public string Name { get; set; }

    public string Value { get; set; }
    public Cookie(string name, string value)
    {
            Guard.AgainstNull(name, "name");
             Guard.AgainstNull(value, "value");
    this.Name = name;
    this.Value = value;

    }
    public override string ToString() => $"{this.Name}={this.Value}";
}
