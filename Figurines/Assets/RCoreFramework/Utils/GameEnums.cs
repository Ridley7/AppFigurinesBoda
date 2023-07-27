
public class GameEnums {

    public enum MessagesTypes
    {
        Empty = 0,
        PressHeadButton = 1,
        PressFigurinButton = 2,
        PressPoseButton = 3,
        EndProcessLoading = 4,
    }

    public enum Senders : uint
    {
        CabezaButton = 1,
        FigurinButton = 2,
        PoseButton = 3,
        AnimationLoading = 4
    }

    public enum TipoAmigo
    {
        LOZANO,
        NOELIA,
        MESADO,
        ALBAE,
        MANOLO,
        ALBAF,
        LEXU,
        LUIS,
        PARRAS,
        JULIAN,
        ANDREA,
        OTHER

    }

    public enum TipoFigurin
    {
        A,
        B,
        C,
        D,
        E,
        F,
        G,
        H,
        I,
        J
    }

    public enum TipoPose
    {
        PERFIL,
        FRENTE,
        SENTADO,
        TODAS
    }

    public enum DireccionAnimacion
    {
        UP,
        DOWN
    }

    public enum AnimacionInicial : int
    {
        NO_PLAYED = 0,
        PLAYED = 1
    }
}
