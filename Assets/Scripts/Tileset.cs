using UnityEngine;

//Changes the materials of objects per level to spice up the scenery.
public class Tileset : MonoBehaviour
{
    [SerializeField] Texture[] walls;
    [SerializeField] Texture[] floors;
    [SerializeField] Texture[] ceilings;
    [SerializeField] Texture[] blocks;
    [SerializeField] Material WallMaterial;
    [SerializeField] Material FloorMaterial;
    [SerializeField] Material CeilingMaterial;
    [SerializeField] Material BlockMaterial;
    void Start()
    {
        int pallete = ScoreGame.level % 4;
        WallMaterial.mainTexture = walls[pallete];
        FloorMaterial.mainTexture = floors[pallete];
        CeilingMaterial.mainTexture = ceilings[pallete];
        BlockMaterial.mainTexture = blocks[pallete];
    }
}
