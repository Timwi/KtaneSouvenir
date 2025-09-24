namespace Souvenir;

public abstract class QuestionBase(string question)
{
    protected string _text = question;
    public virtual string DebugText => _text;
    public abstract void SetQuestion(SouvenirModule souvenir);
}
