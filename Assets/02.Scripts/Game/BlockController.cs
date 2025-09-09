using UnityEngine;

public class BlockController : MonoBehaviour
{
    [SerializeField] private Block[] blocks;

    public delegate void OnBlockClicked(int row, int col);
    public OnBlockClicked OnBlockClickedDelegate;

    //1.모든 block을 초기화
    public void InitBlocks()
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i].InitMarker(i, blockIndex =>
            {
                var row = blockIndex / Constants.BlockColumnCount;
                var col = blockIndex % Constants.BlockColumnCount;
                OnBlockClickedDelegate?.Invoke(row, col);
            });
        }
    }

    //2.특정 block에 마커 표시 기능
    public void PlaceMaker(Block.MarkerType markerType, int row, int col)
    {
        var blockIndex = row * Constants.BlockColumnCount + col;
        blocks[blockIndex].SetMarker(markerType);
    }

    //3.특정 block에 배경색을 설정
    public void SetBlockColor()
    {
        // TODO: 게임 로직이 완성되면 구현
    }
}
