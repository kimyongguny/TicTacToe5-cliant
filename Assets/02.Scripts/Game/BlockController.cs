using UnityEngine;

public class BlockController : MonoBehaviour
{
    [SerializeField] private Block[] blocks;

    public delegate void OnBlockClicked(int row, int col);
    public OnBlockClicked OnBlockClickedDelegate;

    //1.��� block�� �ʱ�ȭ
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

    //2.Ư�� block�� ��Ŀ ǥ�� ���
    public void PlaceMaker(Block.MarkerType markerType, int row, int col)
    {
        var blockIndex = row * Constants.BlockColumnCount + col;
        blocks[blockIndex].SetMarker(markerType);
    }

    //3.Ư�� block�� ������ ����
    public void SetBlockColor()
    {
        // TODO: ���� ������ �ϼ��Ǹ� ����
    }
}
